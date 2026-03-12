const express = require('express');
const router = express.Router();

const orderController = require('../controllers/orderController');


// ORDERS CRUD
router.get('/', orderController.getOrders);
router.get('/:id', orderController.getOrder);

router.post('/', orderController.createOrder);

router.put('/:id', orderController.updateOrder);

router.delete('/:id', orderController.deleteOrder);


module.exports = router;