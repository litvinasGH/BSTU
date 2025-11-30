const nodemailer = require('nodemailer');

let config = {};

let email = '';
let password = '';
let toemail = '';

const init = (config) => {
    email = config.email || '';
    password = config.password || '';
    toemail = config.toemail || '';
}






const send = (message) => {
    return new Promise((resolve, reject) => {


        const transporter = nodemailer.createTransport({
            service: 'gmail',
            auth: {
                user: email,
                pass: password
            },
            secure: true
        });

        const mailOptions = {
            from: email,
            to: toemail,
            subject: 'm0603 module',
            text: message
        };

        transporter.sendMail(mailOptions, (err, info) => {
            if (err) {
                reject(err);
            }
            if (info) {
                resolve(info);
            }
        });
    })

}


module.exports = { init, send };