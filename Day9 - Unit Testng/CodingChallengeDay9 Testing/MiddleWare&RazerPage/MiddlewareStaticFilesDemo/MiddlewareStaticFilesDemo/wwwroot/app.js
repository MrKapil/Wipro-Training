(async function () {
    try {
        const res = await fetch('/api/hello');
        const data = await res.json();
        document.getElementById('msg').textContent = data.message;
    } catch (err) {
        document.getElementById('msg').textContent = 'Failed to load message.';
    }
})();
