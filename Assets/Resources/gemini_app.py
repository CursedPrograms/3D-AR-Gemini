from flask import Flask, render_template, request
import json
import google.generativeai as genai
import webbrowser

app = Flask(__name__)

# Load API key and configure genai library
def load_api_key(file_path='./keys.json'):
    try:
        with open(file_path, 'r') as file:
            api_keys = json.load(file)
            google_api_key = api_keys.get('google_api_key', '')
            genai.configure(api_key=google_api_key)
            print("API keys loaded successfully.")
    except FileNotFoundError:
        print(f"Error: The file {file_path} does not exist.")
    except json.JSONDecodeError:
        print(f"Error: Unable to decode JSON in the file {file_path}.")
    except Exception as e:
        print(f"An unexpected error occurred: {e}")

load_api_key('./keys.json')
model = genai.GenerativeModel('gemini-pro')

@app.route('/')
def index():
    return render_template('gemini.html')

@app.route('/generate', methods=['POST'])
def generate():
    prompt = request.form['prompt']

    if not prompt:
        return render_template('gemini.html', error='Prompt cannot be empty')

    response = model.generate_content(prompt, stream=True)
    
    # Make sure to resolve the response before accessing the final attributes
    response.resolve()
    
    generated_text = ''.join([part.text for part in response.parts])

    return render_template('gemini.html', prompt=prompt, generated_text=generated_text)

if __name__ == "__main__":
    webbrowser.open('http://127.0.0.1:5000/')
    app.run(debug=True, use_reloader=False)
