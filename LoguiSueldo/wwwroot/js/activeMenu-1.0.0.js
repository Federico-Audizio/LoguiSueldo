///** add active class and stay opened when selected */
//var url = window.location;

//// for sidebar menu entirely but not cover treeview
//$('ul.sidebar-menu a').filter(function () {
//    return this.href == url;
//}).parent().addClass('active');

//// for treeview
//$('ul.treeview-menu a').filter(function () {
//    return this.href == url;
//}).parentsUntil(".sidebar-menu > .treeview-menu").addClass('active');


//// for sidebar menu entirely but not cover treeview
//$('li.sidebar-menu a').filter(function () {
//    return this.href == url;
//}).parent().addClass('active');

//// for treeview
//$('li.treeview-menu a').filter(function () {
//    return this.href == url;
//}).parentsUntil(".sidebar-menu > .treeview-menu").addClass('active');




/** add active class and stay opened when selected */
//var url = window.location;
var res = encodeURI(window.location);
var vectorUrl = res.split("/", 5);

if (vectorUrl[4] == "Create" || vectorUrl[4] == "Edit" || vectorUrl[4] == "ImprimirCobro") {
    vectorUrl[4] = "Index";
}
else if (vectorUrl[4] == "" || vectorUrl[4] == null) {
    vectorUrl[4] = "Index";
}
//if (vectorUrl[4] == "Create")
//{
//    vectorUrl[4] = "Create";
//}
//else if (vectorUrl[4] == "Edit")
//{
//    vectorUrl[4] = "Edit";
//}
//else {

//}
//var url = "http://" + vectorUrl[2] + "/" + vectorUrl[3] + "/Index";
var url = "http://" + vectorUrl[2] + "/" + vectorUrl[3] + "/" + vectorUrl[4];

// for sidebar menu entirely but not cover treeview
$('ul.sidebar-menu a').filter(function () {
    return this.href == url;
}).parent().addClass('active');

//Top bar
$('ul.navbar-nav a').filter(function () {
    return this.href == url;
}).parent().addClass('active');

// for treeview
$('ul.treeview-menu a').filter(function () {
    return this.href == url;
}).parentsUntil(".sidebar-menu > .treeview-menu").addClass('active');