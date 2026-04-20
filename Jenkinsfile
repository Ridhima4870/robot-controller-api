pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                dir('robot-controller-api') {
                    bat 'dotnet restore'
                    bat 'dotnet build --configuration Release'
                }
            }
        }

        stage('Run API') {
            steps {
                dir('robot-controller-api') {
                    bat 'start /B dotnet run --urls=http://localhost:5190'
                }
                sleep 15
            }
        }

        stage('Test with Newman') {
            steps {
                dir('robot-controller-api') {
                    bat 'npm install -g newman'
                    bat 'npx newman run "Robot Controller API.postman_collection.json" -e "4.3D robot api.postman_environment.json" -r cli,html,json --reporter-html-export newman-report.html --reporter-json-export results.json'
                }
            }
        }
    }
}