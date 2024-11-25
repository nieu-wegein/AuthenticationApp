$(function () {
	$(".select-all-checkbox").on("click", function () {
		const isChecked = $(this).prop("checked");

		$(".row-checkbox").each(function () {
			$(this).prop("checked", isChecked);
		});
	});

	$(".row-checkbox").on("click", function () {
		$(".select-all-checkbox").prop("checked", false);
	});

	$(".block-button").on("click", function () {
		changeStatus("Blocked");
	});

	$(".unblock-button").on("click", function () {
		changeStatus("Active");
	});

	$(".delete-button").on("click", function () {
		var rowList = $("tr:has(.row-checkbox:checked)");
		var emailList = [];

		if (rowList.length > 0) {
			rowList.each(function () {
				emailList.push($(this).find(".row-checkbox").val());
			})

			fetch("", {
				method: "DELETE",
				body: JSON.stringify(emailList),
				headers: {
					"Accept": "application/json",
					"Content-Type": "application/json"
				}
			}).then((res) => {
				if (res.ok) {
					rowList.remove();
				}
				else if (res.redirected) {
					window.location.href = res.url
				}
			})
		}
	});
});


function changeStatus(status) {
	var rowList = $("tr:has(.row-checkbox:checked)");
	var statusList = rowList.find(".td-status");
	var emailList = [];

	if (rowList.length > 0) {

		rowList.each(function () {
			emailList.push($(this).find(".row-checkbox").val());
		});

		fetch("", {
			method: "PUT",
			body: JSON.stringify({ "EmailList": emailList, "Status": status }),
			headers: {
				"Accept": "application/json",
				"Content-Type": "application/json"
			}
		}).then((res) => {
			if (res.ok) {
				if (status == "Blocked")
					rowList.addClass("blocked");
				else
					rowList.removeClass("blocked");
				statusList.text(status);
			}
			else if (res.redirected) {
				window.location.href = res.url
			}
		});
	}
}