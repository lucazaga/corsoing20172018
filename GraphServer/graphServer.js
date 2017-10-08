var neo4j = require('neo4j-driver').v1;

//import { neo4j } from 'neo4j-driver';

// Create a driver instance, for the user neo4j with password neo4j.
// It should be enough to have a single driver per database per application.
var driver = neo4j.driver("bolt://localhost:7687", neo4j.auth.basic("neo4j", "admin123"));

// Register a callback to know if driver creation was successful:
driver.onCompleted = function () {
  console.log('Driver creation was successful!');
};

// Register a callback to know if driver creation failed.
// This could happen due to wrong credentials or database unavailability:
driver.onError = function (error) {
  console.log('Driver instantiation failed', error);
};

// Create a session to run Cypher statements in.
// Note: Always make sure to close sessions when you are done using them!
var session = driver.session();

// Run a Cypher statement, reading the result in a streaming manner as records arrive:
session
  .run('MATCH (n:Person) RETURN n')
  .subscribe({
    onNext: function (record) {
      console.log(record.toObject());
    },
    onCompleted: function () {
      session.close();
    },
    onError: function (error) {
      console.log(error);
    }
  });

// or
// the Promise way, where the complete result is collected before we act on it:
// session
//   .run('MERGE (james:Person {name : {nameParam} }) RETURN james.name AS name', {nameParam: 'James'})
//   .then(function (result) {
//     result.records.forEach(function (record) {
//       console.log(record.get('name'));
//     });
//     session.close();
//   })
//   .catch(function (error) {
//     console.log(error);
//   });