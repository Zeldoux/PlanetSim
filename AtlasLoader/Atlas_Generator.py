from PIL import Image
import os
import json
import math

# Class to represent a rectangle (each sprite frame)
class Rectangle:
    def __init__(self, width, height, name):
        self.width = width
        self.height = height
        self.name = name
        self.x = 0
        self.y = 0

# Function to calculate the minimum square atlas size dynamically
def calculate_atlas_size(frames):
    total_area = sum(frame.width * frame.height for frame in frames)
    atlas_side = math.ceil(math.sqrt(total_area))
    
    # Optionally, round up to the nearest power of 2
    atlas_side = 2 ** math.ceil(math.log2(atlas_side))
    return atlas_side, atlas_side  # Return square dimensions (width, height)

# Function to pack the rectangles (sprite frames) into the atlas
def pack_sprites(frames, atlas_width, atlas_height):
    packed_frames = []
    free_spaces = [(0, 0, atlas_width, atlas_height)]  # Start with one free space representing the whole atlas

    # Sort the frames by largest area (largest width * height) to smallest
    frames.sort(key=lambda r: r.width * r.height, reverse=True)

    for frame in frames:
        for space in free_spaces:
            x, y, w, h = space
            if frame.width <= w and frame.height <= h:
                # Place the frame
                frame.x = x
                frame.y = y
                packed_frames.append(frame)

                # Update free spaces
                free_spaces.remove(space)
                if w - frame.width > 0:
                    free_spaces.append((x + frame.width, y, w - frame.width, h))  # Add remaining width
                if h - frame.height > 0:
                    free_spaces.append((x, y + frame.height, frame.width, h - frame.height))  # Add remaining height
                break
        else:
            raise Exception(f"Could not place frame {frame.name}")

    return packed_frames

# Create the atlas and update metadata dynamically
def create_atlas_with_metadata(sprite_folder, output_atlas, output_json):
    metadata = {
        "frames": {},
        "meta": {
            "app": "https://github.com/piskelapp/piskel/",
            "version": "1.0",
            "image": output_atlas,
            "format": "RGBA8888",
            "size": {"w": 0, "h": 0}  # Will set this after atlas creation
        }
    }

    # Iterate through each sprite folder
    for sprite in os.listdir(sprite_folder):
        sprite_path = os.path.join(sprite_folder, sprite)
        if os.path.isdir(sprite_path):  # Check if it's a directory
            # Load the sprite sheet and JSON metadata
            for filename in os.listdir(sprite_path):
                if filename.endswith(".png"):
                    sprite_sheet_path = os.path.join(sprite_path, filename)
                    with Image.open(sprite_sheet_path) as img:
                        sprite_sheet = img

                if filename.endswith(".json"):
                    json_path = os.path.join(sprite_path, filename)
                    with open(json_path, 'r') as json_file:
                        sprite_data = json.load(json_file)

            # Process each frame based on the JSON metadata
            frames = []
            for frame_name, frame_info in sprite_data["frames"].items():
                frame_rect = frame_info["frame"]
                frame_width = frame_rect["w"]
                frame_height = frame_rect["h"]
                
                # Create a Rectangle instance for each frame
                frames.append(Rectangle(frame_width, frame_height, frame_name))

            # Calculate the minimum square atlas size
            atlas_width, atlas_height = calculate_atlas_size(frames)
            print(f"Calculated atlas size for {sprite}: {atlas_width}x{atlas_height}")

            # Pack the frames into the atlas
            packed_frames = pack_sprites(frames, atlas_width, atlas_height)

            # Create an empty atlas image
            atlas = Image.new('RGBA', (atlas_width, atlas_height))

            # Paste the frames into the atlas and update metadata
            for frame in packed_frames:
                frame_info = sprite_data["frames"][frame.name]["frame"]
                atlas.paste(sprite_sheet, (frame.x, frame.y), sprite_sheet.crop((frame_info["x"], frame_info["y"], frame_info["x"] + frame.width, frame_info["y"] + frame.height)))

                # Add frame metadata
                frame_data = {
                    "frame": {
                        "x": frame.x,
                        "y": frame.y,
                        "w": frame.width,
                        "h": frame.height
                    },
                    "rotated": False,
                    "trimmed": False,
                    "spriteSourceSize": {
                        "x": 0,
                        "y": 0,
                        "w": frame.width,
                        "h": frame.height
                    },
                    "sourceSize": {
                        "w": frame.width,
                        "h": frame.height
                    }
                }
                metadata["frames"][frame.name] = frame_data

            # Save the final atlas image
            atlas.save(output_atlas)
            print(f"Atlas saved to {output_atlas}")

            # Update the meta size after the atlas creation
            metadata["meta"]["size"]["w"] = atlas_width
            metadata["meta"]["size"]["h"] = atlas_height

            # Save the updated metadata
            with open(output_json, 'w') as outfile:
                json.dump(metadata, outfile, indent=4)
            print(f"Metadata saved to {output_json}")

# Example usage
sprite_folder = "Sprites/"  # e.g., "sprites/"
output_atlas = "atlas.png"  # Output atlas file
output_json = "output_metadata.json"  # Output metadata file

create_atlas_with_metadata(sprite_folder, output_atlas, output_json)
