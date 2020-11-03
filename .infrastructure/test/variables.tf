variable "project" {}

variable "credentials_file" {}

variable "region" {
    type = string
    default = "us-east1"
}

variable "email_address" {
}

variable "zone" {
    type = string
    default = "us-east1-b"
}

variable "vpc_name" {
    type = string
    default = "default"
}

variable "environment" {
    type = string
    default = "prod"
}

variable "boot_image_name" {
}

variable "docker_declaration" {
}

variable "domain" {
}

variable "machine_types" {
    type = map
    default = {
        test = "f1-micro"
        prod = "f1-micro"
    }
}

variable "port_number" {
  type = list(string)
  default = ["80", "443"]
}