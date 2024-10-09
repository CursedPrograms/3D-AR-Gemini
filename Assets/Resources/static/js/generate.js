async function generate() {
    const prompt = document.getElementById("prompt").value;
    const response = await fetch("/generate", {
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
        },
        body: new URLSearchParams({ prompt }),
    });
    const result = await response.json();

    if (result.generated_text) {
        const messages = document.getElementById("messages");
        const synthMessage = document.createElement("div");
        synthMessage.className = "synth-message";
        synthMessage.innerHTML = `<strong>SynthiaGPT:</strong><p>${result.generated_text}</p>`;
        messages.appendChild(synthMessage);
    }
}