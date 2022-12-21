﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//this is function to call the dialog###################################################################################################################################
function ShowCountryCreateModal() {
    $.ajax({
        url: "/country/CreateModalForm",
        type: 'get',
        success: function (response) {
            $("#DivCreateDialog").html(response);
            ShowCreateModalForm();
        }
    });
    return;
}


//this is function to call the dialog for city

function ShowCityCreateModal() {

    var lstCountryCtrl = document.getElementById('lstCountryId');
    var countryid = lstCountryCtrl.options[lstCountryCtrl.selectedIndex].value;

    $.ajax({
        url: "/city/CreateModalForm?countryid=" + countryid,
        type: 'get',
        success: function (response) {
            $("#DivCreateDialog").html(response);
            ShowCreateModalForm();
        }
    })

}



//============================================= this is for fill cities based on countries lists START ===================================================================*@

function FillCities(lstCountryCtrl, lstCityId) {
    var lstCities = $("#" + lstCityId);
    lstCities.empty();

    lstCities.append($('<option/>',
        {
            value: null,
            text: "Select City"
        }));

    var selectedCountry = lstCountryCtrl.options[lstCountryCtrl.selectedIndex].value;

    if (selectedCountry != null && selectedCountry != '') {
        $.getJSON('/Customer/GetCitiesByCountry', { countryId: selectedCountry }, function (cities) {
            if (cities != null && !jQuery.isEmptyObject(cities)) {
                $.each(cities, function (index, city) {
                    lstCities.append($('<option/>',
                        {
                            value: city.value,
                            text: city.text
                        }));
                });
            };
        });
    }
    return;
}

//============================================= this is for fill cities based on countries lists END ===================================================================*@

                //============================================= this is for photo upload START ===================================================================*@

$(".custom-file-input").on("change", function () {

    var fileName = $(this).val().split("\\").pop();

    document.getElementById('PreviewPhoto').src = window.URL.createObjectURL(this.files[0]);

    document.getElementById('PhotoUrl').value = fileName;

});

//============================================= this is for photo upload END ===================================================================*@


//for dialog ------------------------------------------------------------------
function ShowCreateModalForm() {
    $("#DivCreateDialogHolder").modal('show');
    return;
}



function submitModalForm() {
    var btnSubmit = document.getElementById('btnSubmit');
    btnSubmit.click();
}


//first this method gets the refrence to the back btn then  raises the click event of that btn so that the modal dialog closes and focus back to the form
function refreshCountryList() {
    var btnBack = document.getElementById('dupBackBtn');
    btnBack.click();
    FillCountries("lstCountryId");
}


//first this method gets the refrence to the back btn then  raises the click event of that btn so that the modal dialog closes and focus back to the form / same as above method
//but here we find country list by passing the id on getElementById

function refreshCityList() {
    var btnBack = document.getElementById('dupBackBtn');
    btnBack.click();
    var lstCountryCtrl = document.getElementById('lstCountryId')
    FillCities(lstCountryCtrl, "lstCity");
}



//this to refresh the same dialog list at the same time
function FillCountries(lstCountryId) {

    var lstCountries = $("#" + lstCountryId);
    lstCountries.empty();

    lstCountries.append($('<option/>', {
        value: null,
        text: "Select Country"
    }));

    $.getJSON("/country/GetCountries", function (countries) {
        if (countries != null && !jQuery.isEmptyObject(countries)) {

            $.each(countries, function (index, country) {
                lstCountries.append($('<option/>', {
                    value: country.value,
                    text: country.text
                }));
            });
        };
    });
   
}
//for dialog end ------------------------------------------------------------------