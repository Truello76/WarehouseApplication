var editKlient = new Vue({
    template: '',
    el: '#test2',
    data() {
        return {
            modalTitle: 't',
            modalId: 0,
            modalName: 'i',
            modalSurname: 'n',
            modalFirm: 'f',
            modalEmail: 'e',
            message: 'Hello Vue!'
        }
    },
    methods: {
        edit(tmp) {
            this.modalTitle = 'Edytuj klienta';
            //this.modalId = tmp.Id;
            this.modalName = tmp;//.Imie;
            //this.modalSurname = tmp.Nazwisko;
            //this.modalFirm = tmp.Firma;
            //this.modalEmail = tmp.Email;
        },
        reverseMessage: function () {
            this.message = this.message.split('').reverse().join('')
        },
        apiPost() {
            //axios.post("http://localhost:11533/api" + "testApiKlient", { KlientImie: this.message }).then((response) => {alert(response.data);});
        }
    }
});