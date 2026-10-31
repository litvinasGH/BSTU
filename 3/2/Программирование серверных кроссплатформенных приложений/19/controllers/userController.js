const userModel = require('../models/userModel');

exports.getUsers = (req, res) => {
    res.json(userModel.getAll());
};

exports.getUser = (req, res) => {
    res.json(userModel.getById(req.params.id));
};

exports.createUser = (req, res) => {
    const user = userModel.create(req.body);
    res.json(user);
};

exports.updateUser = (req, res) => {
    const user = userModel.update(req.params.id, req.body);
    res.json(user);
};

exports.deleteUser = (req, res) => {
    userModel.delete(req.params.id);
    res.json({message:"User deleted"});
};