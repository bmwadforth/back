pipeline {
    agent {
        label 'docker'
    }
    stages {
        stage('build') {
            agent {
                docker { image 'golang' }
            }
            steps {
                sh 'go version'
            }
        }
        stage('Deploy') {
            when {
                branch 'master'
            }
            steps {
                echo 'Deploying'
            }
        }
    }
}
