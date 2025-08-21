const https = require('https');
const fs = require('fs');
const options = {
key: fs.readFileSync('key.pem'),
cert: fs.readFileSync('cert.pem')
};

const server = https.createServer(options, (req, res) => {
res.writeHead(200);
res.end('Hello from your secure Node.js HTTPS server!');
});

server.listen(3443, () => {
console.log('HTTPS server running at [URL]/');
});