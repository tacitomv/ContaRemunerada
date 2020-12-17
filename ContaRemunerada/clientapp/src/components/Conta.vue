<template>
    <div class="mb-5">
        <h1>Conta #{{ account }}</h1>
        <h2>Fundos: {{funds}}</h2>
        <table class="table table-striped">
            <tr>
                <th>Valor</th>
                <th>Tipo</th>
                <th>Status</th>
                <th>Descrição</th>
            </tr>
            <tr v-for="(transaction, index) in transactions" v-bind:key="index">
                <td>{{transaction.value}}</td>
                <td>{{getTransactionType(transaction.type)}}</td>
                <td>{{getTransactionStatus(transaction.status)}}</td>
                <td>{{transaction.description}}</td>
            </tr>
        </table>
        <button class="btn btn-primary mr-2" @click="box = 1">Depositar</button>
        <button class="btn btn-secondary mr-2" @click="box = 2">Retirar</button>
        <button class="btn btn-info" @click="box  = 3">Pagar</button>

        <div v-if="box == 1">
            <div class="card mt-5">
                <div class="card-body">
                    <h2>Depósito</h2>
                    <div class="form-group">
                        <label class="form-label">Valor</label>
                        <input type="number" value="" class="form-control" v-model="valor" />
                    </div>
                    <button class="btn btn-primary mr-2" @click="depositar">Depositar</button>
                </div>
            </div>
        </div>

        <div v-if="box == 2">
            <div class="card mt-5">
                <div class="card-body">
                    <h2>Retirada</h2>
                    <div class="form-group">
                        <label class="form-label">Valor</label>
                        <input type="number" value="" class="form-control" v-model="valor" />
                    </div>
                    <button class="btn btn-secondary mr-2" @click="retirar">Retirar</button>
                </div>
            </div>
        </div>

        <div v-if="box == 3">
            <div class="card mt-5">
                <div class="card-body">
                    <h2>Pagamento</h2>
                    <div class="form-group">
                        <label class="form-label">Valor</label>
                        <input type="number" value="" class="form-control" v-model="valor" />
                    </div>
                    <div class="form-group">
                        <label class="form-label">O que estou pagando</label>
                        <input type="text" value="" class="form-control" v-model="descricao" />
                    </div>
                    <button class="btn btn-info" @click="pagar">Pagar</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    export default {
        name: 'Conta',
        props: {
            account: Number
        },
        data() {
            return {
                funds: 0,
                transactions: [],
                box: 9,
                valor: 0,
                descricao: ''
            }
        },
        methods: {
            depositar() {
                axios.post('/accounts/deposit',
                    {
                        contaId: parseInt(this.account),
                        valor: parseFloat(this.valor)
                    })
                    .then((response) => {
                        this.transactions.push(response.data);
                        this.funds += response.data.value;
                        this.clearFields();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            retirar() {
                axios.post('/accounts/withdraw',
                    {
                        contaId: parseInt(this.account),
                        valor: parseFloat(this.valor)
                    })
                    .then((response) => {
                        this.transactions.push(response.data);
                        this.funds -= response.data.value;
                        this.clearFields();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            pagar() {
                axios.post('/accounts/pay',
                    {
                        contaId: parseInt(this.account),
                        valor: parseFloat(this.valor),
                        descricao: this.descricao
                    })
                    .then((response) => {
                        this.transactions.push(response.data);
                        this.funds -= response.data.value;
                        this.clearFields();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            getTransactionStatus(status) {
                switch (status) {
                    case 0: return "Concluída";
                    case 1: return "Em análise";
                    case 2: return "Negada";
                    case 3: return "Cancelada";
                }
            },
            getTransactionType(type) {
                switch (type) {
                    case 0: return "Depósito";
                    case 1: return "Resgate";
                    case 2: return "Pagamento";
                    case 3: return "Remuneração";
                }
            },
            clearFields() {
                this.box = 9;
                this.valor = 0;
                this.descricao = '';
            },
            getTransactions() {
                axios.get('/accounts/transactions?contaId=' + this.account)
                    .then((response) => {
                        this.transactions = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            getFunds() {
                axios.get('/accounts/funds?contaId=' + this.account)
                    .then((response) => {
                        this.funds = response.data.value;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            }
        },
        mounted() {
            this.getFunds();
            this.getTransactions();
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
