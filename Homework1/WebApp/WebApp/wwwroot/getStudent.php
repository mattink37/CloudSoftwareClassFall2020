<?php
error_reporting(E_ALL);
// PHP Data Objects(PDO) Sample Code:
try {
    $conn = new PDO("sqlsrv:server = tcp:cloudcomputingclassdb.database.windows.net,1433; Database = CloudComputingClassDB", "mattink37", "a:U7wp_a");
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
}
catch (PDOException $e) {
    print("Error connecting to SQL Server.");
    die(print_r($e));
}

// SQL Server Extension Sample Code:
$connectionInfo = array("UID" => "mattink37", "pwd" => "a:U7wp_a", "Database" => "CloudComputingClassDB", "LoginTimeout" => 30, "Encrypt" => 1, "TrustServerCertificate" => 0);
$serverName = "tcp:cloudcomputingclassdb.database.windows.net,1433";
$conn = sqlsrv_connect($serverName, $connectionInfo);
$q = $_REQUEST["q"];
$tsql= "SELECT id, grade FROM Grades WHERE id = $q";
$getResults = sqlsrv_query($conn, $tsql);

$data = array();
while ($row = sqlsrv_fetch_array($getResults, SQLSRV_FETCH_ASSOC))
{
  $data[] = $row;
}
echo json_encode($data);

/*$grades = array();
$count = 0;
$studentID = -1;
while ($row = sqlsrv_fetch_array($getResults, SQLSRV_FETCH_ASSOC)) {
     $studentID = $row['id'];
     $grades[$count] = $row['grade'];
     $count++;
     }
echo "<h1>Grades for Student $studentID</h1>";
echo "<table>";
echo "<tr>";
echo "<th>Grade</th>";
echo "<tr>";
for ($i = 0; $i < count($grades); $i++) {
  echo "<tr>";
  echo "<td>$grades[$i]</td>";
  echo "</tr>";
}
echo "</table>";
?>*/