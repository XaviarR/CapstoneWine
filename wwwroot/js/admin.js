function showSubscriptionsTable() {
	document.getElementById("subscriptionsTable").style.display = "block";
	document.getElementById("winesTable").style.display = "none";
	document.getElementById("ordersTable").style.display = "none";
}

function showWinesTable() {
	document.getElementById("subscriptionsTable").style.display = "none";
	document.getElementById("winesTable").style.display = "block";
	document.getElementById("ordersTable").style.display = "none";
}

function showOrdersTable() {
	document.getElementById("subscriptionsTable").style.display = "none";
	document.getElementById("winesTable").style.display = "none";
	document.getElementById("ordersTable").style.display = "block";
}