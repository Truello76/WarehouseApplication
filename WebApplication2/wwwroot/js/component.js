var tmp = Vue.component('header-component', {
    props: ['klienci'],
    template: '<tr><td v-for="klient in klienci">{{klient}}</td><td><button class="btn btn-primary" data-toggle="modal" data-target="#add" v-on:click="edit(klient.Imie)">Edytuj</button><button class="btn btn-danger" onclick="document.getElementById("delete").style.display="block"">Usuń</button></td></tr>'
});