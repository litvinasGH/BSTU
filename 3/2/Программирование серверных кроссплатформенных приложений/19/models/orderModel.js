let orders = [];

module.exports = {

    getAll: () => orders,

    getById: (id) => orders[id],

    create: (order) => {
        orders.push(order);
        return order;
    },

    update: (id, order) => {
        orders[id] = order;
        return order;
    },

    delete: (id) => {
        orders.splice(id, 1);
    }
};