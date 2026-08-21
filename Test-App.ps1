$baseUrl = "http://localhost:5000"
$endpoints = @(
    "/",
    "/Home/Properties",
    "/Home/About",
    "/Home/Contact",
    "/Auth/Login"
)

Write-Host "Starting HTTP Integration Tests against $baseUrl..."

$allPassed = $true

foreach ($endpoint in $endpoints) {
    $url = "$baseUrl$endpoint"
    try {
        $response = Invoke-WebRequest -Uri $url -UseBasicParsing -MaximumRedirection 0 -ErrorAction Stop
        if ($response.StatusCode -eq 200) {
            Write-Host "[PASS] GET $url returned 200 OK" -ForegroundColor Green
        } else {
            Write-Host "[WARN] GET $url returned $($response.StatusCode)" -ForegroundColor Yellow
        }
    } catch {
        $ex = $_.Exception.Response
        if ($ex) {
            Write-Host "[FAIL] GET $url returned $($ex.StatusCode)" -ForegroundColor Red
        } else {
            Write-Host "[FAIL] GET $url failed: $_" -ForegroundColor Red
        }
        $allPassed = $false
    }
}

# Test a protected route (should return 302 Redirect to Login)
try {
    $adminUrl = "$baseUrl/AdminProperties"
    $response = Invoke-WebRequest -Uri $adminUrl -UseBasicParsing -MaximumRedirection 0 -ErrorAction Stop
    Write-Host "[FAIL] GET $adminUrl should have redirected but returned $($response.StatusCode)" -ForegroundColor Red
    $allPassed = $false
} catch {
    if ($_.Exception.Response.StatusCode -eq 302) {
        Write-Host "[PASS] GET $adminUrl correctly returned 302 Redirect to Login (Authentication works)" -ForegroundColor Green
    } else {
        Write-Host "[FAIL] GET $adminUrl failed with unexpected error: $_" -ForegroundColor Red
        $allPassed = $false
    }
}

if ($allPassed) {
    Write-Host "`nAll tests passed successfully! The application views are rendering without errors." -ForegroundColor Green
} else {
    Write-Host "`nSome tests failed." -ForegroundColor Red
}
