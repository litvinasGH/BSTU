let books = [];

module.exports = {

    getAll: () => books,

    getById: (id) => books[id],

    create: (book) => {
        books.push(book);
        return book;
    },

    update: (id, book) => {
        books[id] = book;
        return book;
    },

    delete: (id) => {
        books.splice(id, 1);
    }
};