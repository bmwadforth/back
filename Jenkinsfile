pipeline {
    agent { 
        label 'any'
        docker { image 'golang' }
    }
    stages {
        stage('build') {
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
