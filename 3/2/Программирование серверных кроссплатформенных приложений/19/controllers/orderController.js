const orderModel = require('../models/orderModel');

exports.getOrders = (req, res) => {
    res.json(orderModel.getAll());
};

exports.getOrder = (req, res) => {
    res.json(orderModel.getById(req.params.id));
};

exports.createOrder = (req, res) => {
    res.json(orderModel.create(req.body));
};

exports.updateOrder = (req, res) => {
    res.json(orderModel.update(req.params.id, req.body));
};

exports.deleteOrder = (req, res) => {
    orderModel.delete(req.params.id);
    res.json({message:"Order deleted"});
};