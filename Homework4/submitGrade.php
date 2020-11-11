<?php
try {
    $conn = new PDO("sqlsrv:server = tcp:cloudcomputingclassdb.database.windows.net,1433; Database = CloudComputingClassDB", "mattink37", "a:U7wp_a");
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
}
catch (PDOException $e) {
    print("Error connecting to SQL Server.");
    die(print_r($e));
}

$connectionInfo = array("UID" => "mattink37", "pwd" => "a:U7wp_a", "Database" => "CloudComputingClassDB", "LoginTimeout" => 30, "Encrypt" => 1, "TrustServerCertificate" => 0);
$serverName = "tcp:cloudcomputingclassdb.database.windows.net,1433";
$conn = sqlsrv_connect($serverName, $connectionInfo);
$id = $_REQUEST["id"];
$grade = $_REQUEST["grade"];
$tsql= "insert into Grades (id, grade) values ('$id', '$grade');";
$submit = sqlsrv_query($conn, $tsql);
?>