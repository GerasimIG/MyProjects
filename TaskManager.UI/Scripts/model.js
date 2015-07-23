function ViewModel() {
    var self = this;
    self.categories = ko.observableArray([]);
    self.newCategoryText = ko.observable();
    self.chosenCategoryId = ko.observable();
    self.chosenCategoryTasks = ko.observableArray([]),
    self.newTaskText = ko.observable();
    self.gotError = ko.observable(false);
    self.error = ko.observable("");

    self.authenticated = ko.observable(false);
    self.username = ko.observable(null);
    self.password = ko.observable(null);

    self.getCategories = function () {
        $.getJSON("/api/categories/", function (data) {
            var mappedCategories = $.map(data, function (item) { return new Category(item) });
            self.categories(mappedCategories);
            if (self.categories().length > 0) {
                goToCategory(self.categories()[0]);
            }
        });
    };

    self.getCategories();
};

function Category(category) {
    this.id = category.Id;
    this.text = category.Text;
}

function Task(task) {
    this.newSubTaskText = ko.observable();
    this.subTasks = ko.observableArray([]);
    this.id = task.Id;
    this.text = task.Text;
    this.isSubTasksOpened = ko.observable(false);
    this.subTasksOpenHideText = ko.observable("Show sub tasks");
    this.isFinished = ko.observable(task.IsFinished);
};

function SubTask(subTask) {
    this.id = subTask.Id;
    this.text = subTask.Text;
    this.isFinished = ko.observable(subTask.IsFinished);
    this.taskId = subTask.TaskId;
};

var model = new ViewModel();

$(document).ready(function () {
    
    ko.applyBindings(new ViewModel());
    setDefaultCallbacks(function (data) {
        if (data) {
            console.log("---Begin Success---");
            console.log(JSON.stringify(data));
            console.log("---End Success---");
        } else {
            console.log("Success (no data)");
        }
        model.gotError(false);
    },
        function (statusCode, statusText) {
            console.log("Error: " + statusCode + " (" + statusText + ")");
            model.error(statusCode + " (" + statusText + ")");
            model.gotError(true);
        });
});
