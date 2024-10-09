from bs4 import BeautifulSoup

def clean_text(text, is_markdown=False):
    """
    Cleans the given text by removing HTML tags and unwanted characters.

    Parameters:
    - text (str): The input text.
    - is_markdown (bool): Whether the text is in Markdown format.

    Returns:
    - str: The cleaned text.
    """
    # If the text is in Markdown format, convert it to plain text
    if is_markdown:
        # You can add your own logic here for Markdown to plain text conversion
        # For simplicity, let's remove Markdown indicators (e.g., **, *) and links
        cleaned_text = BeautifulSoup(text, "html.parser").get_text(separator=" ")
        cleaned_text = cleaned_text.replace("**", "").replace("*", "")
    else:
        # Remove HTML tags
        soup = BeautifulSoup(text, "html.parser")
        cleaned_text = soup.get_text(separator=" ")

    # Remove unwanted characters
    cleaned_text = cleaned_text.replace("\n", " ").strip()

    return cleaned_text
