properties([pipelineTriggers([githubPush()])])

pipeline {
    agent any

    stages {
        stage('hello') {
            steps {
                // Get some code from a GitHub repository
                echo 'hello'
            }
        }
		stage('workplacePath'){
			steps{
				echo "${env.WORKSPACE}"
			}
		}
        stage('Delete Workspace') {
            steps {
                deleteDir()
            }
        }

        stage('Git Clone') {
            steps {
                // Get some code from a GitHub repository
                git branch: 'main',
                url: 'https://github.com/ProductivityTools-Salaries/ProductivityTools.Salaries.Api'
            }
        }
        stage('Build solution') {
            steps {
                bat(script: 'dotnet publish ProductivityTools.Salaries.Api.sln -c Release', returnStdout: true)
            }
        }
        stage('Delete databse migration directory') {
            steps {
                bat('if exist "C:\\Bin\\DbMigration\\PTSalaries.Api" RMDIR /Q/S "C:\\Bin\\DbMigration\\PTSalaries.Api"')
            }
        }
        stage('Copy database migration files') {
            steps {
                bat('xcopy "ProductivityTools.Salaries.Api.DbUp\\bin\\Release\\net9.0\\publish\\" "C:\\Bin\\DbMigration\\PTSalaries.Api\\" /O /X /E /H /K')
            }
        }

       stage('Run databse migration files') {
            steps {
                bat('C:\\Bin\\DbMigration\\PTSalaries.Api\\ProductivityTools.Salaries.Api.DbUp.exe')
            }
        }

        stage('Create page on the IIS') {
            steps {
                powershell('''
                function CheckIfExist($Name){
                    cd $env:SystemRoot\\system32\\inetsrv
                    $exists = (.\\appcmd.exe list sites /name:$Name) -ne $null
                    Write-Host $exists
                    return  $exists
                }
                
                 function Create($Name,$HttpbBnding,$PhysicalPath){
                    $exists=CheckIfExist $Name
                    if ($exists){
                        write-host "Web page already existing"
                    }
                    else
                    {
                        write-host "Creating app pool"
                        .\\appcmd.exe add apppool /name:$Name /managedRuntimeVersion:"v4.0" /managedPipelineMode:"Integrated"
                        write-host "Creating webage"
                        .\\appcmd.exe add site /name:$Name /bindings:http://$HttpbBnding /physicalpath:$PhysicalPath
                        write-host "assign app pool to the website"
                        .\\appcmd.exe set app "$Name/" /applicationPool:"$Name"


                    }
                }
                Create "PTSalaries" "*:8005"  "C:\\Bin\\IIS\\PTSalaries"                
                ''')
            }
        }

        stage('Stop page on the IIS') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd stop site /site.name:PTSalaries')
            }
        }

		stage('Delete PTSalaries IIS directory') {
            steps {
              powershell('''
                if ( Test-Path "C:\\Bin\\IIS\\PTSalaries")
                {
                    while($true) {
                        if ( (Remove-Item "C:\\Bin\\IIS\\PTSalaries" -Recurse *>&1) -ne $null)
                        {  
                            write-output "removing failed we should wait"
                        }
                        else 
                        {
                            break 
                        } 
                    }
                  }
              ''')

            }
        }

        stage('Copy web page to the IIS Bin directory') {
            steps {
                bat('xcopy "ProductivityTools.Salaries.Api\\bin\\Release\\net9.0\\publish\\" "C:\\Bin\\IIS\\PTSalaries\\" /O /X /E /H /K')				              
            }
        }

        stage('Start website on IIS') {
            steps {
                bat('%windir%\\system32\\inetsrv\\appcmd start site /site.name:PTSalaries')
            }
        }

        stage('Create Login PTSalaries on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q "CREATE LOGIN [IIS APPPOOL\\PTSalaries] FROM WINDOWS WITH DEFAULT_DATABASE=[PTSalaries];"')
             }
        }

        stage('Create User PTSalaries on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q " USE PTSalaries;  CREATE USER [IIS APPPOOL\\PTSalaries]  FOR LOGIN [IIS APPPOOL\\PTSalaries];"')
             }
        }

        stage('Give DBOwner permissions on SQL2022') {
             steps {
                 bat('sqlcmd -S ".\\SQL2022" -q "USE PTSalaries;  ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\\PTSalaries];"')
             }
        }

        stage('byebye') {
            steps {
                // Get some code from a GitHub repository
                echo 'byebye'
            }
        }
    }
	post {
		always {
            emailext body: "${currentBuild.currentResult}: Job ${env.JOB_NAME} build ${env.BUILD_NUMBER}\n More info at: ${env.BUILD_URL}",
                recipientProviders: [[$class: 'DevelopersRecipientProvider'], [$class: 'RequesterRecipientProvider']],
                subject: "Jenkins Build ${currentBuild.currentResult}: Job ${env.JOB_NAME}"
		}
	}
}
