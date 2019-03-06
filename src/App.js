import React, { Component } from 'react';
import Battle from './Battle.js';



class App extends Component {
  constructor(props) {
    super(props);
  }



  render() {

    // Finally render the Unity component and pass
    // the Unity content through the props.

    return <Battle />;
  }
}

export default App;
