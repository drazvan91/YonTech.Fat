angular.module('todoApp', [])
    .controller('TodoListController', function ($http, $timeout) {
        var todoList = this;

        var pendingItems = [];

        todoList.todos = [
            { text: 'learn AngularJS', done: true },
            { text: 'build an AngularJS app', done: false }];

        todoList.addTodo = function () {
            todoList.todos.push({ text: todoList.todoText, done: false });
            todoList.todoText = '';
        };

        todoList.remaining = function () {
            var count = 0;
            angular.forEach(todoList.todos, function (todo) {
                count += todo.done ? 0 : 1;
            });
            return count;
        };

        todoList.archive = function () {
            var oldTodos = todoList.todos;
            todoList.todos = [];
            angular.forEach(oldTodos, function (todo) {
                if (!todo.done) todoList.todos.push(todo);
            });
        };

        todoList.isLoading = function () {
            return pendingItems.length > 0;
        }

        todoList.addFromBackend = function () {
            var time = Math.floor((Math.random() * 2000) + 2000);;
            var newItem = {
                id: time,
                text: "Item from backend "+time,
                done: false
            };

            pendingItems.push(newItem);
            
            $http.get("/fullPageRequestDemo/longRequest?timeToSleep="+time)
                .then(function () {
                    todoList.todos.push(newItem);

                    var index = pendingItems.indexOf(newItem);
                    if (index > -1) {
                        pendingItems.splice(index, 1);
                    }
                });
        }

        todoList.addDelayed = function () {
            var time = Math.floor((Math.random() * 2000) + 2000);;
            var newItem = {
                id: time,
                text: "Delayed item " + time,
                done: false
            };

            pendingItems.push(newItem);

            $timeout(function () {
                    todoList.todos.push(newItem);

                    var index = pendingItems.indexOf(newItem);
                    if (index > -1) {
                        pendingItems.splice(index, 1);
                    }
                },
                time);
        }
    });