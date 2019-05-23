<template>
  <v-container>
    <v-layout
      text-xs-center
      wrap
    >
      <v-flex xs12>
        <v-img
          :src="require('../assets/funda_logo.png')"
          class="my-3"
          contain
          height="200"
        ></v-img>
      </v-flex>

      <v-flex xs12>
        <h1 class="display-2 font-weight-bold mb-3">
          Ranking the Real Estate Agents
        </h1>
        <p class="subheading font-weight-regular">
          Want to find out the top 10 of biggest real estate agents in Amsterdam?
          Check how many objects they have listed, with or without garden. Try and find out!
        </p>
      </v-flex>

      <RankingTable/>

    </v-layout>
  </v-container>
</template>

<script>
  import RankingTable from './RankingTable'

  export default {
    name: 'Ranking',
    components: {
      RankingTable
    }
  }
</script>

<style>

</style>
