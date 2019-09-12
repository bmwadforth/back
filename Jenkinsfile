pipeline {
  agent {
    docker {
      image 'golang'
    }

  }
  stages {
    stage('Echo') {
      steps {
        sh '''echo "HELLO"
'''
      }
    }
  }
}