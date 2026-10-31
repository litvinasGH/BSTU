let users = [];

module.exports = {

    getAll: () => users,

    getById: (id) => users[id],

    create: (user) => {
        users.push(user);
        return user;
    },

    update: (id, user) => {
        users[id] = user;
        return user;
    },

    delete: (id) => {
        users.splice(id, 1);
    }
};