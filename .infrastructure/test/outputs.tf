output "Public_IP_Address" {
  value = google_compute_address.apa3_api_test_lb_static_ip.address
}