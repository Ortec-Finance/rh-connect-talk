# Introduction 
 
 Ortec Finance conducts a research towards High Performance Cloud Computing. 
 This repository contains prototypes of synchronous and asynchronous cloud applications.

# Prerequisites

1. RedHat Openshift 4.X
2. KEDA operator for Openshift.
3. Red Hat Integration - AMQ Broker for RHEL 8 (Multiarch) operator Openshift.
4. Serverless operator for Openshift.
5. KEDA  instance in "keda" namespace.
6. KNativeServing instance in "knative-serving" namespace.


# Getting Started
```bash
 Prototype
│   ├── gitops # Contains everything GitOps related
│   │   ├── base 
│   │   │   ├── deployment.yaml  
│   │   │   ├── ...                 # Abstraction of all the yamls needed for deployment of the system.
│   │   │   └── service.yaml        # Does not contain any fixed urls etc.
│   │   └── overlays
│   │       ├── develop                 # Contains all the necessities for the dev. process. Tag : develop.
│   │       │   └── kustomization.yaml
│   │       └── main                    # Actuall urls/names/images for deployment. Tag : latest.
│   │           └── kustomization.yaml
│   ├── ProjectOne
│   │   
│   └── ProjectTwo
```

Keep base as an abstraction of the deployment. All the changes for your deployment can be done via Kustomize. 

# Deploy

1. Navigate to the ../{PrototypeName}/gitops/overlays/{develop or main}/
2. Open kustomization.yaml
3. Replace the indicated values with your URL/Namespace etc.
4. Save. 
5. oc apply -k .


# Build 

If you want to modify the code an build your own image

1. Navigate to the ../{PrototypeName}/{ProjectName}
2. docker build -rm -t {IMAGE_NAME:IMAGE_TAG}
3. docker push {IMAGE_NAME}
4. Navigate to the ../{PrototypeName}/gitops/overlays/{develop or main}/kustomize.yaml
5. Change images/newName to {IMAGE_NAME}
6. Change images/newTag to {IMAGE_TAG}
7. Save. 
8. oc apply -k .


# Slides

{LINK TO SLIDES}
