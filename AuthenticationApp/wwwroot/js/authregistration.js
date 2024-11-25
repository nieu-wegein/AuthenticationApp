$(function () {
	var form = $(".registration-form");
	form.on("submit", function (e) {
		e.preventDefault();
		$.post("", form.serialize()).done(function () {
			window.location.href = form.attr("data-redirect");
		}).fail(function (resp) {
			var data = resp.responseJSON;
			var errorContainer = $(".error-message");
			errorContainer.text(data.detail);
		});
	});
})