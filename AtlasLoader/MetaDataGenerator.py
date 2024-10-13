import os
import json

def generate_metadata_for_sprite(sprite_folder, frame_width, frame_height, output_json=None):
    metadata = {
        "frames": [],
        "image": sprite_folder,  # The name of the folder or animation
        "frameWidth": frame_width,
        "frameHeight": frame_height
    }

    # Set a default duration (you can customize this later for different animations)
    duration = 100

    # Process all PNG files inside the sprite folder
    for index, filename in enumerate(sorted(os.listdir(sprite_folder)), start=1):
        if filename.endswith(".png"):
            frame_data = {
                "filename": filename,
                "duration": duration,
                "frameIndex": index
            }
            metadata["frames"].append(frame_data)

    # Save metadata to JSON (each animation gets its own JSON file)
    if not output_json:
        output_json = f"{sprite_folder}_metadata.json"
    
    with open(output_json, 'w') as outfile:
        json.dump(metadata, outfile, indent=4)

    print(f"Metadata for {sprite_folder} saved to {output_json}")

def generate_metadata_for_all_sprites(root_folder, frame_width, frame_height):
    # Process each subfolder (each subfolder is a sprite animation)
    for sprite_folder in os.listdir(root_folder):
        full_sprite_folder_path = os.path.join(root_folder, sprite_folder)

        if os.path.isdir(full_sprite_folder_path):
            print(f"Processing sprite animation: {sprite_folder}")
            generate_metadata_for_sprite(full_sprite_folder_path, frame_width, frame_height)

# Example usage
root_folder = "path_to_sprite_animations"  # Folder containing subfolders for each animation
frame_width = 64  # Set the width of each frame
frame_height = 64  # Set the height of each frame

generate_metadata_for_all_sprites(root_folder, frame_width, frame_height)
