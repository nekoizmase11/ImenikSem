

    $(document).ready(function () {

        var KontaktiPoStrani = 10;
    var odabranaStrana = 1;
    var stringPretrage;

        function definisiPaginaciju(podaci) {
        $('.lista').empty();

            if (podaci.ImaPrethodni) {
                var prethodni = $('<li class="page-item"><a id="prethodni" class="page-link" href="#" class="elementPaginacije">&laquo</a></li>');
    $('.lista').append(prethodni);
}

            $.each(podaci.RaspoloziveStrane, function (index, value) {
                var raspolozivaStrana;

                if (value == podaci.TrenutnaStrana) {
        raspolozivaStrana = $('<li class="page-item"><a id="' + value + '" class="page-link" style="background-color:#00203FFF; color:white; font-weight: bold;" href="#">' + value + '</a></li>');

}
                else {
        raspolozivaStrana = $('<li class="page-item"><a id="' + value + '" class="page-link" href="#">' + value + '</a></li>');
}
$('.lista').append(raspolozivaStrana);
});

            if (podaci.ImaSledeci) {
                var sledeci = $('<li class="page-item"><a id="sledeci" class="page-link" href="#">&raquo</a></li>');
    $('.lista').append(sledeci);
}
}

        function prikaziKontakte(podaci) {
            var redovi = ``;

            $.each(podaci.Kontakti, function (index, value) {
        console.log(value);
    var red = `
                    <tr>
        <td>
            `+ value.Ime + `
                        </td>
        <td>
            `+ value.Prezime + `
                        </td>
        <td>
            `+ value.Broj + `
                        </td>
        <td>
            <a href="/Kontakti/Izmeni/`+ value.Id + `">Izmeni</a> |
                            <a onclick="return confirm('Da li zelite da obrisete kontakt: `+ value.Ime + ` ` + value.Prezime + `? ')" href="/Kontakti/Obrisi/` + value.Id + `">Obrisi</a>
        </td>
    </tr>
    `;
    redovi += red;
});


$(".table").html(`
            <tbody>
        <tr>
            <th>
                Ime
                </th>
            <th>
                Prezime
                </th>
            <th>
                Broj
                </th>
            <th></th>
        </tr>
        ` + redovi + `
            </tbody>
    `);

}

        function ucitajKontakte() {

            var urlString;
            if (stringPretrage !== undefined) {
        urlString = `/Kontakti/GetPretraga?strana=` + odabranaStrana + `&brKontakataPoStrani=` + KontaktiPoStrani + `&stringPretrage=` + stringPretrage;
            } else {
        urlString = `/Kontakti/GetStrana?strana=` + odabranaStrana + `&brKontakataPoStrani=` + KontaktiPoStrani;
}

fetch(urlString,
                {
        method: 'GET',
                    headers: {'Content-Type': 'application/json' }
})
                .then(odgovor => {
                    if (!odgovor.ok) {
                        return Promise.reject(odgovor);
}
return odgovor.json();
})
                .then(podaci => {
                    if (podaci) {
        //console.log("Uspesno");
        console.log(podaci);
    definisiPaginaciju(podaci);
    prikaziKontakte(podaci);
}
})
                .catch(odgovor => {
        console.log(odgovor);
    alert("Doslo je do greske, probajte opet.")
});
}


//***********************************************gore deinisanje funkcija koje su sad pozivane


//Ako je tabela prazna ucitaj je, inicijacija
        if ($(".table").find('tbody').length < 1) {
        odabranaStrana = 1;
    KontaktiPoStrani = 10;

    ucitajKontakte();
}

//Event za klik na stranicu paginacije
        $(".lista").on("click", "a", function (event) {

            var odabrano = event.target.id
            if (odabrano == "prethodni") {

        odabranaStrana = odabranaStrana - 1;

            } else if (odabrano == "sledeci") {

        odabranaStrana = odabranaStrana + 1;

            } else {

        odabranaStrana = parseInt(event.target.id);
}

ucitajKontakte();
});

//Event za promenu broja prikazanih kontakata
        $("#dropdownlista").change(function () {

        KontaktiPoStrani = $('#dropdownlista option:selected').val();

    ucitajKontakte();
});

//Pretraga
        $("#pretraga").on('search', function (event) {

        odabranaStrana = 1;

    stringPretrage = $(this).val();
    ucitajKontakte();
});

});

