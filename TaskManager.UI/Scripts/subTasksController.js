var subTasksUrl = "/api/sub-tasks/";

var addSubTask = function (item) {
    if (/\S/.test(item.newSubTaskText())) {        
        var data = ko.toJSON({ Text: item.newSubTaskText(), TaskId: item.id });
        sendRequest(subTasksUrl, "POST", data, function () {
            item.newSubTaskText("");
            getChosenCategoryTasks();
        });
    }
};

var removeSubTask = function(item) {
    var url = subTasksUrl + item.id;
    sendRequest(url, "DELETE", null, function () {
        getChosenCategoryTasks();
    });
};

var updateSubTaskStatus = function (item) {
    var data = ko.toJSON({ Id : item.id });
    sendRequest(subTasksUrl, "PUT", data, function () {
        getChosenCategoryTasks();
    });
};

function findById(source, id) {
    for (var i = 0; i < source.length; i++) {
        if (source[i].id === id) {
            return source[i];
        }
    }
    throw "Couldn't find object with id: " + id;
}
