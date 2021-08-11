<template>
  <div>
    <p>
      {{counter}}
    </p>
    <table>
      <thead>
        <tr>
          <th>Date</th>
          <th>Temperature C</th>
          <th>Summary</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in weatherForecastList" :key="item.date">
          <td>{{item.date}}</td>
          <td>{{item.temperatureC}}</td>
          <td>{{item.summary}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>

import axios from 'axios';

export default {
  name: "Home",
  data(){
    return {
      weatherForecastList: null,
      counter: 0
    }
  },
  mounted(){
    axios
      .get("api/WeatherForecast")
      .then(response => {
        this.weatherForecastList = response.data;
        console.log(response.data);
        this.counter++;
      })
      .catch(function (error) {
        console.log(error);
      });
  }
};
</script>
