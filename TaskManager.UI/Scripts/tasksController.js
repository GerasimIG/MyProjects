var tasksUrl = "/api/tasks/";

var addTask = function() {
    if (/\S/.test(model.newTaskText())) {
    
        var data = ko.toJSON({ Text: model.newTaskText, CategoryId: model.chosenCategoryId });

        sendRequest(tasksUrl, "POST", data, function() {
            getChosenCategoryTasks();
            model.newTaskText("");
        });
    }
};

var removeTask = function (item) {
    var url = tasksUrl + item.id;
    sendRequest(url, "DELETE", null, function() {
        getChosenCategoryTasks();
    });
};

var getTaskSubTasks = function (item) {
    if (item.isSubTasksOpened()) {
        item.isSubTasksOpened(false);
        item.subTasksOpenHideText("Show sub tasks");
        return;
    };
    var url = tasksUrl + item.id;
    sendRequest(url, "GET", null, function(data) {
        var mappedSubTasks = $.map(data, function (item) { return new SubTask(item) });
        item.subTasks(mappedSubTasks);
        item.isSubTasksOpened(true);
        item.subTasksOpenHideText("Hide sub tasks");
    });
};


var updateTaskStatus = function (item) {
    var data = ko.toJSON({ Id: item.id });
    sendRequest(tasksUrl, "PUT", data, function () {
        getChosenCategoryTasks();
    });
};

