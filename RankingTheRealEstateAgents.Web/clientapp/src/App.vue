<template>
  <v-app>
    <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase">
        <span>Funda</span>
        <span class="font-weight-light">Ranker</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-content>
      <Ranking/>
    </v-content>
  </v-app>
</template>

<script>
import Ranking from './components/Ranking'

export default {
  name: 'App',
  components: {
    Ranking
  },
  data () {
    return {
      //
    }
  }
}
</script>

<style>
  .theme--light.v-toolbar {
    background-color: #f7a100;
  }
</style>
