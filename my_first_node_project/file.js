// const fs = require('fs');
// fs.readFile('example.txt', 'utf8'(error, data), => {

//     if(err){
//         console.error('Error reading:', err);
//         return;
//     }
//     console.log('File contensts:', data);
// });

// fs.writeFile('output.txt', 'Hello,node.js!',(err) => {
//     if(err){
//         console.error('Error writing file:', err);
//         return;
//     }
//     console.error('File successfully writtem!');
// });

const http = require('http');
    const server = http.createServer((req, res) => {

        res.writeHead(200,{'Content-Type':'text/plain'});
        res.end('Hello from Node.js HTTP Server!');
    });

    server.listen(3000, () => {
        console.log('server running at [URL]/');
    });

    