angular.module("todoApp", []).controller("TodoListController", function($scope, $http, $timeout) {
  var todoList = this;

  $scope.safeApply = function(fn) {
    var phase = this.$root.$$phase;
    if (phase == "$apply" || phase == "$digest") {
      if (fn && typeof fn === "function") {
        fn();
      }
    } else {
      this.$apply(fn);
    }
  };

  var pendingItems = [];

  todoList.todos = [{ text: "learn AngularJS", done: true }, { text: "build an AngularJS app", done: false }];

  todoList.addTodo = function() {
    todoList.todos.push({ text: todoList.todoText, done: false });
    todoList.todoText = "";
  };

  todoList.remaining = function() {
    var count = 0;
    angular.forEach(todoList.todos, function(todo) {
      count += todo.done ? 0 : 1;
    });
    return count;
  };

  todoList.archive = function() {
    var oldTodos = todoList.todos;
    todoList.todos = [];
    angular.forEach(oldTodos, function(todo) {
      if (!todo.done) todoList.todos.push(todo);
    });
  };

  todoList.isLoading = function() {
    return pendingItems.length > 0;
  };

  var addFromBackend = function(funcToUse) {
    var time = Math.floor(Math.random() * 2000 + 2000);
    var newItem = {
      id: time,
      text: "Item from backend " + time,
      done: false
    };

    pendingItems.push(newItem);

    funcToUse("/fullPageRequestDemo/longRequest?timeToSleep=" + time).then(function() {
      todoList.todos.push(newItem);

      var index = pendingItems.indexOf(newItem);
      if (index > -1) {
        pendingItems.splice(index, 1);
      }

      $scope.safeApply();
    });
  };

  todoList.addFromBackendUsingHttp = function() {
    addFromBackend($http.get);
  };

  todoList.addFromBackendUsingFetch = function() {
    addFromBackend(fetch);
  };

  todoList.addDelayed = function() {
    var time = Math.floor(Math.random() * 2000 + 2000);
    var newItem = {
      id: time,
      text: "Delayed item " + time,
      done: false
    };

    pendingItems.push(newItem);

    $timeout(function() {
      todoList.todos.push(newItem);

      var index = pendingItems.indexOf(newItem);
      if (index > -1) {
        pendingItems.splice(index, 1);
      }
    }, time);
  };

  todoList.clickOnArea = function(e) {
    console.log(e);
    var posX = $(e.target).offset().left,
      posY = $(e.target).offset().top;
    todoList.clickX = e.pageX - posX;
    todoList.clickY = e.pageY - posY;
  };
});
