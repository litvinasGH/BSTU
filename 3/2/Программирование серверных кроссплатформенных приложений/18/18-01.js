const { Sequelize } = require('sequelize');
const { ORM } = require('./orm18');
const http = require('http');
const fs = require('fs');

const PORT = 3000;

const sequelize = new Sequelize('KVV_PDB', 'KVV', '1111', {
    dialect: 'oracle',
    host: 'localhost',
    port: 1522,
    dialectOptions: {
        connectString: 'localhost:1522/KVV_PDB'
    },
    pool:{
        max:5,
        min:0,
        idle:10000
    },
    logging:false
});

const {Faculty, Pulpit, Teacher, Subject, Auditorium_type, Auditorium} = ORM(sequelize);

const sendResponse = (res,data)=>{
    res.writeHead(200,{'Content-Type':'application/json'});
    res.end(JSON.stringify(data));
};

const server = http.createServer(async (req,res)=>{

try{
    console.log(`${req.method} ${req.url}`);
    if(req.method==='GET' && req.url==='/'){
        res.writeHead(200,{'Content-Type':'text/html'});
        res.end(fs.readFileSync('./index.html','utf8'));
        return;
    }

    if(req.method==='GET' && req.url==='/api/faculties'){
        sendResponse(res, await Faculty.findAll());
        return;
    }

    if(req.method==='GET' && req.url==='/api/pulpits'){
        sendResponse(res,
            await Pulpit.findAll({
                include:{
                    model: Subject,
                    as: 'subjects'
                }
            })
        );
        return;
    }

    if(req.method==='GET' && req.url==='/api/subjects'){
        sendResponse(res, await Subject.findAll());
        return;
    }

    if(req.method==='GET' && req.url==='/api/auditoriumstypes'){
        sendResponse(res, await Auditorium_type.findAll());
        return;
    }

    if(req.method==='GET' && req.url==='/api/auditoriums'){
        sendResponse(res, await Auditorium.findAll());
        return;
    }


    if (['POST','PUT'].includes(req.method)) {

    const body = await new Promise((resolve, reject) => {

        let data = '';

        req.on('data', chunk => {
            data += chunk;
        });

        req.on('end', () => resolve(data));

        req.on('error', reject);

    });

    req.body = body ? JSON.parse(body) : {};
}


    if(req.method==='POST' && req.url==='/api/faculties'){
        sendResponse(res, await Faculty.create(req.body));
        return;
    }

    if(req.method==='POST' && req.url==='/api/pulpits'){
        sendResponse(res, await Pulpit.create(req.body));
        return;
    }

    if(req.method==='POST' && req.url==='/api/subjects'){
        console.log(req.body);
        const subject = await Subject.create(req.body);

        console.log(subject);
        sendResponse(res, subject);
        return;
    }

    if(req.method==='POST' && req.url==='/api/auditoriumstypes'){
        sendResponse(res, await Auditorium_type.create(req.body));
        return;
    }

    if(req.method==='POST' && req.url==='/api/auditoriums'){
        sendResponse(res, await Auditorium.create(req.body));
        return;
    }


    if(req.method==='PUT' && req.url==='/api/faculties'){
        await Faculty.update(req.body,{where:{FACULTY:req.body.FACULTY}});
        sendResponse(res, await Faculty.findByPk(req.body.FACULTY));
        return;
    }

    if(req.method==='PUT' && req.url==='/api/pulpits'){
        await Pulpit.update(req.body,{where:{PULPIT:req.body.PULPIT}});
        sendResponse(res, await Pulpit.findByPk(req.body.PULPIT));
        return;
    }

    if(req.method==='PUT' && req.url==='/api/subjects'){
        await Subject.update(req.body,{where:{SUBJECT:req.body.SUBJECT}});
        sendResponse(res, await Subject.findByPk(req.body.SUBJECT));
        return;
    }

    if(req.method==='PUT' && req.url==='/api/auditoriumstypes'){
        await Auditorium_type.update(req.body,{where:{AUDITORIUM_TYPE:req.body.AUDITORIUM_TYPE}});
        sendResponse(res, await Auditorium_type.findByPk(req.body.AUDITORIUM_TYPE));
        return;
    }

    if(req.method==='PUT' && req.url==='/api/auditoriums'){
        await Auditorium.update(req.body,{where:{AUDITORIUM:req.body.AUDITORIUM}});
        sendResponse(res, await Auditorium.findByPk(req.body.AUDITORIUM));
        return;
    }


    if(req.method==='DELETE' && req.url.startsWith('/api/faculties/')){
        const id=req.url.split('/')[3];
        const obj=await Faculty.findByPk(id);
        await Faculty.destroy({where:{FACULTY:id}});
        sendResponse(res,obj);
        return;
    }

    if(req.method==='DELETE' && req.url.startsWith('/api/pulpits/')){
        const id=req.url.split('/')[3];
        const obj=await Pulpit.findByPk(id);
        await Pulpit.destroy({where:{PULPIT:id}});
        sendResponse(res,obj);
        return;
    }

    if(req.method==='DELETE' && req.url.startsWith('/api/subjects/')){
        const id=req.url.split('/')[3];
        const obj=await Subject.findByPk(id);
        await Subject.destroy({where:{SUBJECT:id}});
        sendResponse(res,obj);
        return;
    }

    if(req.method==='DELETE' && req.url.startsWith('/api/auditoriumtypes/')){
        const id=req.url.split('/')[3];
        const obj=await Auditorium_type.findByPk(id);
        await Auditorium_type.destroy({where:{AUDITORIUM_TYPE:id}});
        sendResponse(res,obj);
        return;
    }

    if(req.method==='DELETE' && req.url.startsWith('/api/auditoriums/')){
        const id=req.url.split('/')[3];
        const obj=await Auditorium.findByPk(id);
        await Auditorium.destroy({where:{AUDITORIUM:id}});
        sendResponse(res,obj);
        return;
    }

    res.writeHead(404);
    res.end();

}
catch(e){
    res.writeHead(500,{'Content-Type':'application/json'});
    res.end(JSON.stringify({error:e.message}));
}

});


sequelize.authenticate()
.then(()=>{
    console.log('connected');
    server.listen(PORT,()=>{
        console.log(`Server running http://localhost:${PORT}`);
    });
})
.catch(console.log);