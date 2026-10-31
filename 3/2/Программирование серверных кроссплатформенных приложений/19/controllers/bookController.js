const bookModel = require('../models/bookModel');

exports.getBooks = (req, res) => {
    res.json(bookModel.getAll());
};

exports.getBook = (req, res) => {
    res.json(bookModel.getById(req.params.id));
};

exports.createBook = (req, res) => {
    res.json(bookModel.create(req.body));
};

exports.updateBook = (req, res) => {
    res.json(bookModel.update(req.params.id, req.body));
};

exports.deleteBook = (req, res) => {
    bookModel.delete(req.params.id);
    res.json({message:"Book deleted"});
};