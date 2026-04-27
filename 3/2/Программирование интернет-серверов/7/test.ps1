# ===== URL =====
$url = "http://localhost:3000/rpc"

# ===== ФУНКЦИЯ ДЛЯ ОТПРАВКИ =====
function Send-RPC($body) {
    $json = $body | ConvertTo-Json -Depth 5
    Invoke-RestMethod -Uri $url -Method Post -ContentType "application/json" -Body $json
}

# ===== ARRAY PARAMS =====
Send-RPC @{jsonrpc="2.0"; method="sum"; params=@(5.05,3.33); id=1}
Send-RPC @{jsonrpc="2.0"; method="sub"; params=@(5.05,3.33); id=2}
Send-RPC @{jsonrpc="2.0"; method="mul"; params=@(5.05,3.33); id=3}
Send-RPC @{jsonrpc="2.0"; method="div"; params=@(5.05,3.33); id=4}

# ===== OBJECT PARAMS =====
Send-RPC @{jsonrpc="2.0"; method="sum"; params=@{x=5.05;y=3.33}; id=5}
Send-RPC @{jsonrpc="2.0"; method="sub"; params=@{x=5.05;y=3.33}; id=6}
Send-RPC @{jsonrpc="2.0"; method="mul"; params=@{x=5.05;y=3.33}; id=7}
Send-RPC @{jsonrpc="2.0"; method="div"; params=@{x=5.05;y=3.33}; id=8}

# ===== SET PRECISION =====
Send-RPC @{jsonrpc="2.0"; method="pre"; params=@{N=3}}

# ===== AFTER PRECISION =====
Send-RPC @{jsonrpc="2.0"; method="sum"; params=@(5.05,3.33); id=9}
Send-RPC @{jsonrpc="2.0"; method="sub"; params=@(5.05,3.33); id=10}
Send-RPC @{jsonrpc="2.0"; method="mul"; params=@(5.05,3.33); id=11}
Send-RPC @{jsonrpc="2.0"; method="div"; params=@(5.05,3.33); id=12}

# ===== SET PRECISION AGAIN =====
Send-RPC @{jsonrpc="2.0"; method="pre"; params=@{N=1}}

# ===== OBJECT AGAIN =====
Send-RPC @{jsonrpc="2.0"; method="sum"; params=@{x=5.05;y=3.33}; id=13}
Send-RPC @{jsonrpc="2.0"; method="sub"; params=@{x=5.05;y=3.33}; id=14}
Send-RPC @{jsonrpc="2.0"; method="mul"; params=@{x=5.05;y=3.33}; id=15}
Send-RPC @{jsonrpc="2.0"; method="div"; params=@{x=5.05;y=3.33}; id=16}

# ===== BATCH =====
$batch = @(
    @{jsonrpc="2.0"; method="pre"; params=@{N=4}},
    @{jsonrpc="2.0"; method="sum"; params=@{x=5.05;y=3.33}; id=17},
    @{jsonrpc="2.0"; method="sub"; params=@{x=5.05;y=3.33}; id=18},
    @{jsonrpc="2.0"; method="mul"; params=@{x=5.05;y=3.33}; id=19},
    @{jsonrpc="2.0"; method="div"; params=@{x=5.05;y=3.33}; id=20}
)

$batchJson = $batch | ConvertTo-Json -Depth 5
Invoke-RestMethod -Uri $url -Method Post -ContentType "application/json" -Body $batchJson