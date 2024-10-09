import os
import shutil
from gtts import gTTS
from .clean_text import clean_text
from .play_audio import play_audio

def text_to_speech(text):
    cleaned_text = clean_text(text)

    # Check if there is any text to speak
    if cleaned_text.strip():
        audio_directory = "output/audio"
        audio_path = os.path.join(audio_directory, "output.mp3")

        try:
            # Create the directory if it doesn't exist
            os.makedirs(audio_directory, exist_ok=True)

            # Save the TTS output to the file
            tts = gTTS(text=cleaned_text, lang='en', slow=False)
            tts.save(audio_path)

            # Play the audio
            play_audio(audio_path)

            print("Text-to-speech conversion successful.")
        except PermissionError as e:
            print(f"PermissionError: {e}. Check file and directory permissions.")
        except Exception as e:
            print(f"Error: {e}")
    else:
        print("No text to speak.")
