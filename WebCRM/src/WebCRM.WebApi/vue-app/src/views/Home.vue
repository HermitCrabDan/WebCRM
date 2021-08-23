<template>
  <div>
      <w-app block>
        <h1>Home</h1>
        <w-flex justify-center fill-width>
          <w-card title="About" style="min-width:600px; max-width:800px">
            <w-accordion :items="items">
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
            content:'Contracts are tied to members and accounts and have associated transactions'
          },
        ],
      }
    }
}
</script>
