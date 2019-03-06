import React, { Component } from 'react';
// Get started by creating a new React
// component and importing the libraries!

import Unity, { UnityContent } from "react-unity-webgl";

class Battle extends Component {
  constructor(props) {
    super(props);

    // Next up create a new Unity Content object to
    // initialise and define your WebGL build. The
    // paths are relative from your index file.

    this.unityContent = new UnityContent(
      "MyGame/Build/MyGame.json",
      "MyGame/Build/UnityLoader.js"
    );

    this.unityContent.on("GameOver", score => {
      this.setState({
        gameOver: true,
        score: score
      })
    })
  }



  render() {

    // Finally render the Unity component and pass
    // the Unity content through the props.

    return <Unity unityContent={this.unityContent} />;
  }
}

export default Battle;
