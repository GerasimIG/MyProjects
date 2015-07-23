var categoriesUrl = "/api/categories/";
var categoryTasksUrl = function(categoryId) {
    return categoriesUrl + categoryId + "/tasks";
};



var addCategory = function () {
    if (/\S/.test(model.newCategoryText())) {
        var data = ko.toJSON({ Text: model.newCategoryText });
        sendRequest(categoriesUrl, "POST", data, function() {
            model.getCategories();
            model.newCategoryText("");
        });
    }
};

var removeCategory = function(item) {
    sendRequest(categoriesUrl + item.id, "DELETE", null, function() {
        model.getCategories();
    });
};

var goToCategory = function(category) {
    model.chosenCategoryId(category.id);
    getChosenCategoryTasks();
};

var getChosenCategoryTasks = function () {
    sendRequest(categoryTasksUrl(model.chosenCategoryId()), "GET", null, function(data) {
        model.chosenCategoryTasks.removeAll();
        var mappedTasks = $.map(data, function (item) { return new Task(item) });
        model.chosenCategoryTasks(mappedTasks);
    });
}

var removeAllFinishedTasks = function () {
    sendRequest(categoryTasksUrl(model.chosenCategoryId()), "DELETE", null, function () {
        getChosenCategoryTasks();
    });
};

