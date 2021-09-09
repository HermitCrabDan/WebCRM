<template>
  <div>
      <w-app block>
        <h1>Home</h1>
        <w-flex justify-center fill-width>
          <w-card title="About" style="min-width:600px; max-width:800px">
            <w-accordion 
              v-model="accordion"
              :items="items">
            </w-accordion>
          </w-card>
        </w-flex>
      </w-app>
  </div>
</template>

<script>

export default {
    name:"Home",
    data(){
      return {
        items:[
          {
            title:'Accounts',
            content:'Accounts can have any number of notes, members, and contracts associated with them'
          },
          {
            title:'Members',
            content:'Members can be tied to registered users of the site, members can only see their own contracts unless they are the primary account member, then they can view all the contracts of the account'
          },
          {
            title:'Contracts',
            content:'Contracts have a contract amount with start and end dates, as well as a payment day of the month are tied to account memberships and have associated transactions and expenses'
          },
          {
            title:'Contract Transactions',
            content:'Transactions have a transaction amount, transaction date as well as a payment year and month'
          },
          {
            title:'Transaction Fees',
            content:'Transactions can have fees recorded for bookkeeping purposes. They are tracked and reported but do not impact the contact paid amounts'
          },
          {
            title:'Contract Expenses',
            content:'This is for bookkeeping and remittance purposes. Expenses are recorded and tracked on the contract, remittance and summary sections of the site but do not impact the contract paid amount'
          },
        ],
        accordion:[true,true,true,true,true,true]
      }
    }
}
</script>
