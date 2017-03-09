# FhirStarter
Is a fork of Furores Spark (https://github.com/furore-fhir/spark). 
FHIRStarter is a lightweight WebApi project which is easy to integrate with a service with several datasources.

The goal is to support the latest STU from nuget along with FhirPath.

Currently more of the code needs to be rewritten to be more futureproof. As the code is now, it might break if the STU is heavily rewritten in its core.

To get going with FhirStarter:

Copy the FhirStarter.Inferno.Template project. This project already is configured with the nuget packages by default. There's even an example how a service can be setup with Conformance and other settings. Keep in mind that an Inferno server can only have one service per Resource type url, since ninject will iniate the first resource in its initation list when a request (asking on the resource) is sent to the Inferno server.

Example: 
Have two Patient services, A and B. 
Both are initated in the same project with the same interface (IFhirService). 
When a request on the url fhir/Patient is received, ninject will either choose A or B. There is no logic to look at conformance (yet) to dermine which service should be accessed.
