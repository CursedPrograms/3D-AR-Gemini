import json
import google.generativeai as genai
from system.tts import text_to_speech
from IPython.display import display, Markdown
import pathlib
import textwrap

def to_markdown(text):
    text = text.replace('•', '  *')
    return Markdown(textwrap.indent(text, '> ', predicate=lambda _: True))

def load_api_key(file_path='./keys.json'):
    try:
        with open(file_path, 'r') as file:
            api_keys = json.load(file)
            google_api_key = api_keys.get('google_api_key', '')

            # Configure the genai library with the Google API key
            genai.configure(api_key=google_api_key)

            print("API keys loaded successfully.")
    except FileNotFoundError:
        print(f"Error: The file {file_path} does not exist.")
    except json.JSONDecodeError:
        print(f"Error: Unable to decode JSON in the file {file_path}.")
    except Exception as e:
        print(f"An unexpected error occurred: {e}")

def main():
    # Example usage:
    load_api_key('./keys.json')

    model = genai.GenerativeModel('gemini-pro')
    
    while True:
        prompt = ""
        prompt_template = "Hi Synthia, talk as if we're having a quick and friendly chat. Here's your prompt:"
        user_input = input(f"Enter a prompt (type 'quit' to exit): ")

        prompt = prompt_template + user_input
        response = model.generate_content(prompt, stream=True)
        
        if prompt.lower() == 'quit':
            break

        # Check if the user wants to exit
        if user_input.lower() == 'quit':
            exit()

        response = model.generate_content(prompt, stream=True)
        response.resolve()  # Resolve the response to complete the iteration

        # Use response.parts instead of response.text
        generated_text = ''.join([part.text for part in response.parts])

        print(generated_text)
        text_to_speech(generated_text)
        # print(response.prompt_feedback)

if __name__ == "__main__":
    main()
