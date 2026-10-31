var http = require('http');
var fs = require('fs');
var url = require('url');
var data_module = require('./DataBaseModule');
const PORT = 5000;

var db = new data_module.DB();
let stopTimeout;
let commitInterval;
let statisticsTimeout;

let serverStats = {
    time_start:null,
    time_finish:null,
    request_count:0,
    commit_count:0
}
let lastStats = {
    time_start:null,
    time_finish:null,
    request_count:0,
    commit_count:0
}

db.on('GET', async(request, response) => {
    response.writeHead(200,{'content-type':'application/json;charset=utf-8'});
    response.end(JSON.stringify(db.select()))
});


db.on('POST', async (request, response) => {
    request.on('data',data=>{
        let dat = JSON.parse(data);
        db.insert(dat);
        response.writeHead(200,{'content-type':'application/json;charset=utf-8'});
        response.end(JSON.stringify(dat));
    });
});


db.on('PUT',async(request,response)=>{
    request.on('data',data=>{
        let dat = JSON.parse(data);
        db.update(dat);
        response.writeHead(200,{'content-type':'application/json;charset=utf-8'});
        response.end(JSON.stringify(db.select()));
    })
})


db.on('DELETE',async(request,response)=>{
    let parsedURL = url.parse(request.url,true);
    const id = parsedURL.query.id;
    if(!id){
        response.writeHead(400,{'content-type':'text/html;charset=utf-8'});
        response.end('<h1>400 Bad request</h1>')
    }
    const deletedRow = await db.delete(id);
    response.writeHead(200,{'content-type':'application/json;charset=utf-8'});
    response.end(JSON.stringify(deletedRow));
});

db.on('COMMIT', async () => {
    db.commit();
    serverStats.commit_count++; 
});


http.createServer(function (request,response){
    if(request){
        serverStats.request_count++;
        //console.log(request.url);
    }
    if(url.parse(request.url).pathname==="/api/db"){
        db.emit(request.method,request,response);
    }
    else if (url.parse(request.url).pathname === "/api/ss") {
        response.writeHead(200, { 'content-type': 'application/json;charset=utf-8' });
        response.end(JSON.stringify(lastStats));
    }
    else if(request.url==="/"){
        let page = fs.readFileSync('./index.html');
        response.writeHead(200,{'content-type':'text/html;charset=utf-8'});
        response.end(page);
    }
    else{
        response.writeHead(404,{'content-type':'text/html'});
        response.end(
            "<h1>404 Not Found</h1>"
        );
    }
}).listen(PORT);

const stopServer = (delay) => {
    stopTimeout = setTimeout(() => {
        console.log("Bye!");
        process.exit(0);

    }, delay);
    stopTimeout.unref();
}

const startCommiting = (interval) => {
    commitInterval = setInterval(() => {
        db.emit('COMMIT');
    }, interval);
    commitInterval.unref();
}

const collectStatistics = (collDelay) => {
    serverStats = {
    time_start:null,
    time_finish:null,
    request_count:0,
    commit_count:0
    }
    serverStats.time_start = new Date(Date.now()).toISOString();
    lastStats = serverStats;
    statisticsTimeout = setTimeout(() => {
        serverStats.time_finish = new Date(Date.now()).toISOString();
        lastStats = {...serverStats};
        console.log("Statistic collected");
        console.log(lastStats);
    }, collDelay);

    statisticsTimeout.unref();
}



process.stdin.setEncoding('utf-8');

// if (process.stdin.isTTY) {
//   process.stdin.setRawMode(false);
// }

console.log("Server running at http://localhost:5000/");

process.stdout.write(`input->`);
process.stdin.on('data', (data) => {
    try{
    const input = data.trim().toLowerCase();
    const [command, flag] = input.split(/\s+/);
    if (flag && isNaN(flag)){
        throw "flag isNaN!";
    }
    switch (command) {
        case 'exit':
            process.exit(0);
        break;

        case 'sd':
            clearTimeout(stopTimeout);
            if (flag) {
                stopServer(parseInt(flag));
            }
            else {
                clearTimeout(stopTimeout);
                console.log("Server stop aborted");
            }
            break;

        case 'sc':
            clearInterval(commitInterval);
            if (flag) {
                startCommiting(parseInt(flag));
            }
            else {
                clearInterval(commitInterval);
                console.log("Commit execution aborted");
            }
            break;

        case 'ss':
            clearTimeout(statisticsTimeout);
            if (flag) {
                collectStatistics(parseInt(flag));
            }
            else {
                clearTimeout(statisticsTimeout);
                console.log("Statistics collection aborted");
            }
            break;

        default:
        throw (`Command "${input}" not found`);
        break;
    }
    }
    catch(err){
        console.log(err);
    }
    finally{
    process.stdout.write(`input->`);
    }

});



