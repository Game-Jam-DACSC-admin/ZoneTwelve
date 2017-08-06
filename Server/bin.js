#!/usr/bin/env node

/**
* Socket Server 核心
**/
const PORT = 3000;
const socket = require('socket.io');
const server = socket({transports:["websocket"]});
const io = server.attach(PORT);

/**
* Socket Center - 處理連線之相關數據
**/
const body = require('./index.js');
const center = io.on('connection', body);

console.log(`Server runing on ${PORT} Port`);
