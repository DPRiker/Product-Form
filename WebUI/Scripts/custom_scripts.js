
var checkbox = $("#Enable")



document.getElementById("Enable").addEventListener("change", myFunction);

function myFunction() {

	// проверим имя изменённого свойства
	if (checkbox.prop("checked") == true) {
		$(".image").css("display", "none")

	}
	else {
		// остальные браузеры

		$(".image").css("display", "block")

	}

}