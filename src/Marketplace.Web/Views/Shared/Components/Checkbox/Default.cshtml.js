const checkboxComponent = document.querySelector(".checkbox > input");

function CheckboxInit()
{
    checkboxComponent.onchange = () => {
        checkboxComponent.setAttribute("value", checkboxComponent.checked);
    }
}

checkboxComponent && CheckboxInit();